# DeveloperListing

This is an api used to consume developer listings

### Below is a list of Technologies used
.Net core 5, Entity Framework Core, Auto Mapper, Moq, xUnit

#### Patterns and Principles
Repository Pattern, Generic Repository Pattern, Unit of Work Pattern, Data transfer objects, caching, paginating and filtering

#### Data Storage
The API currently has a local SQLite datatabase that runs in the app for development and testing. For production we can easily switch to any other backing store by changing repository and nowhere else in the code. This demonstrates the awesomeness of loosely coupled components
