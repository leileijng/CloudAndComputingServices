# CSC Assignment 2

Second Assignment of CSC

The objectives of this assignment are to create The Life Time Talents (TLTT) Saas web application.
By the end of the project, the two functions would be fulfilled:
1. Allow user to register/log in/log off to the application
2. Allow different users to CRUD talent's data



![User Permission](https://i.imgur.com/KvFoIHM.png)



The tasks required to support the two functions are as follow.

Task 1 - Virtual Server
- [ ] Host talent's Saas Web App in AWS EC2

Task 2 - Subscription Management
- [ ] Payment Management using Stripe to accept either free or paid users
- [ ] Discussion Management using Disqus to allow real time commenting for paid users

Task 3 - Image Management
- [ ] Image Storage using AWS S3 to store talents' images
- [ ] Image Recognition using Clarifai to validate images do not fall for NSFW

Task 4 - Database
- [ ] SQL Database using AWS RDS to store talents data and image URL
- [ ] No SQL Database using AWS DynamoDB to store user's subscription data (subscription plan, last paid, and session data)
- [ ] Lifecycle data management using AWS Redshift to export manually talents' and user's data; and import to data warehouse

Task 5 - API Mangement
- [ ] Publish/ Deploy talents' data REST API using Amazon API Gateway

Task 6 - Documentation
- [ ] Document all API endpoint and POSTMAN testing screenshot

Task 7 - Cross Browser Testing
- [ ] Auto test web app using LambdaTest and show proof (screenshot)

Task 8 - System Diagram
- [ ] Draw a detailed architecture system diagram to illustrate the Saas application

Others
- [ ] Authenticate user before accessing the Saas app using AWS Cognito
- [ ] TBD



Developed by Jiang Lei, Franky, Lim Zi Yun, Jorin
