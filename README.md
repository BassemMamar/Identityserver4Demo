# Identityserver4Demo

a very basic example discribe how to use Identityserver4 based on `Identityserver4 » Docs » Setup and Overview`

## Structure

 1. AuthServer
    - An ASP.NET core application 
    - Has `Identityserver4` installed on and register resourses (ApiResource or IdentityResources), clients and users 
    - Support External Authentication like Google 
    - Support User Authentication with OpenID Connect, depend on
     [Quickstart UI repo](https://github.com/IdentityServer/IdentityServer4.Quickstart.UI/tree/release)
    - Self-hosted in the port `5000`

 2. APIDemo
    - An ASP.NET core Web API application 
    - Has an api controller which is used to test the authorization requirement, as well as visualize the claims identity through the eyes of the API
    - Has 'IdentityServer4.AccessTokenValidation' installed to validate JWT and reference tokens from IdentityServer4
    - Self-hosted in the port `5001`

 3. ClientDemo
    - An ASP.NET core Console application 
    - This client is used to requests an access token, and then uses this token to access the API
    - Has 'IdentityModel' installed to encapsulates the OAuth 2.0 protocol interaction in an easy to use API.
    - Contain an example how to use client with GrantTypes `ClientCredentials`, and another one to use client with GrantTypes `ResourceOwnerPassword`

 4. WebAppDemo
    - An ASP.NET core MVC application 
    - This client is used to represent User Authentication with OpenID Connect
    - You can navigate to Cliens view which is Authorized by `Identityserver4`
    - You can login using test users {Username = "alice",Password = "password",} or {Username = "bob",Password = "password",} or by your google account
    - You can do logout to delete `Cookies`


## Tests Cases

1. First test case
   * Run AuthServer
   * Run APIDemo
   * Run ClientDemo

2. Second test case
   * Run AuthServer
   * Run WebAppDemo

