using CXChange.BuildingBlocks.Application.Totp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.CoreElements.Infrastructure.Totp
{
    internal class Totp:ITotp
    {

        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeSpan _timestep = TimeSpan.FromMinutes(10);
        private static readonly Encoding _encoding = new UTF8Encoding(false, true);
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private static byte[] GenerateRandomKey()
        {
            byte[] bytes = new byte[20];
            _rng.GetBytes(bytes);
            return bytes;
        }
        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier, int numberOfDigits = 6)
        {
            // # of 0's = length of pin
            var Mod = (int)Math.Pow(10, numberOfDigits);
            // var Mod = (int)Math.Pow(10, 8);

            // See https://tools.ietf.org/html/rfc4226
            // We can add an optional modifier
            var timestepAsBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var hash = hashAlgorithm.ComputeHash(ApplyModifier(timestepAsBytes, modifier));

            // Generate DT string
            var offset = hash[hash.Length - 1] & 0xf;
            Debug.Assert(offset + 4 < hash.Length);
            var binaryCode = (hash[offset] & 0x7f) << 24
                             | (hash[offset + 1] & 0xff) << 16
                             | (hash[offset + 2] & 0xff) << 8
                             | (hash[offset + 3] & 0xff);
            var a = (binaryCode % Mod).ToString().Length;
            //if ((binaryCode % Mod).ToString().Length != 6)
            //{
            //    var b = binaryCode % Mod;

            //    return int.Parse((binaryCode % Mod).ToString("D6", CultureInfo.InvariantCulture));
            //}
            return binaryCode % Mod;
        }
        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (String.IsNullOrEmpty(modifier))
            {
                return input;
            }

            var modifierBytes = _encoding.GetBytes(modifier);
            var combined = new byte[checked(input.Length + modifierBytes.Length)];
            Buffer.BlockCopy(input, 0, combined, 0, input.Length);
            Buffer.BlockCopy(modifierBytes, 0, combined, input.Length, modifierBytes.Length);
            return combined;
        }

        private static ulong GetCurrentTimeStepNumber()
        {
            var delta = DateTime.UtcNow - _unixEpoch;
            return (ulong)(delta.Ticks / _timestep.Ticks);
        }
        private static ulong GetCurrentTimeStepNumber(TimeSpan _timestep)
        {
            var delta = DateTime.UtcNow - _unixEpoch;
            return (ulong)(delta.Ticks / _timestep.Ticks);
        }
        public int GenerateCode(byte[] securityToken, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }

            // Allow a variance of no greater than 9 minutes in either direction
            var currentTimeStep = GetCurrentTimeStepNumber(_timestep);
            using (var hashAlgorithm = new HMACSHA1(securityToken))
            {
                return ComputeTotp(hashAlgorithm, currentTimeStep, modifier, numberOfDigits);
            }
        }
        public bool ValidateCode(byte[] securityToken, int code, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6)
        {
            if (securityToken == null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            //if (code.ToString().Length != numberOfDigits)
            //{
            //    throw new NotImplementedException(nameof(numberOfDigits));
            //}
            // Allow a variance of no greater than 9 minutes in either direction
            var currentTimeStep = GetCurrentTimeStepNumber(_timestep);
            using (var hashAlgorithm = new HMACSHA1(securityToken))
            {
                for (var i = -2; i <= 2; i++)
                {
                    var computedTotp = ComputeTotp(hashAlgorithm, (ulong)((long)currentTimeStep + i), modifier, numberOfDigits);
                    if (computedTotp == code)
                    {
                        return true;
                    }
                }
            }

            // No match
            return false;
        }
    }
}
