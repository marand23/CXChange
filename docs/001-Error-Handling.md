# 1 Error Handling
## Error handling with exceptions or result class
This document describes the decisions made to use the error handling mechanism in this application
## Descision
Business invariants will be protected by exceptions, we have BuisinessRuleException in the BuildingBlocks
and we will throw this exception in the case of buisiness invariant violation  but in the application layer 
using exceptions for the cases we know that can cause error  is prohibited and we shuoul catch exception and return 
a Result class, and also when catching BuisinessRuleException we should catch and return Result class.
For Other unexpected situations we use exceptions.we will handle exceptions and result class in the 
Api layer by adding an extension method to controller base

