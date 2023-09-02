# VideomaticWebAPI

The web API for the Videomatic app.

This project is currently not being worked in since I can use the API via 
the combination of MediatR and Hangfire/Azure queues.

This module will be developed further when we will need to create React/Angular/WASM front ends.
Because the choice of Blazor server this can be postponed until necessary.

1) Should provide a GraphQL endpoint (my preferred choice)

2) Look into [Chillicream's HotChocolate](https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core)

3) Should provide a REST endpoint (probably necessary because it's simpler?)