## Estimated Time to Completion - 5 Hours

## Assumption
- OrderBook API list is ordered

## If not timeboxed
- Add global exception handling and err page - log to Sentry or Raygun
- Add more validation on presentation layer
- Add auth for API (bearer if auth present)
- Add integration tests for business logic validation

## Steps 
- Restore nuget packages for solution
- Open VS - Set start up project as IR-tech-test.Web
- Open VS - Set start up project as IR-tech-test.Api (IR-tech-test.Api project must also be running, as it is not automatically attached. This was so the API could be independent of MVC, and deployed separately)
- You should see the UI in the debugging session of IR-tect-test.Web 
