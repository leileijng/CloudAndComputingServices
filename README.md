# CSC Assignment 2

Second Assignment of CSC

The objectives of this assignment are to create The Life Time Talents (TLTT) Saas web application.
By the end of the project, the two functions would be fulfilled:
1. Allow user to register/log in/log off to the application
2. Allow different users to CRUD talent's data



![User Permission](https://i.imgur.com/KvFoIHM.png)



The tasks required to support the two functions are as follow.

Task 1 - Virtual Server
- [x] Host talent's Saas Web App in AWS EC2

Task 2 - Subscription Management
- [x] Payment Management using Stripe to accept either free or paid users
- [x] Discussion Management using Disqus to allow real time commenting for paid users

Task 3 - Image Management
- [x] Image Storage using AWS S3 to store talents' images
- [x] Image Recognition using Clarifai to validate images do not fall for NSFW

Task 4 - Database
- [x] SQL Database using AWS RDS to store talents data and image URL
- [x] No SQL Database using AWS DynamoDB to store user's subscription data (subscription plan, last paid, and session data)
- [x] Lifecycle data management using AWS Redshift to export manually talents' and user's data; and import to data warehouse

Task 5 - API Mangement
- [x] Publish/ Deploy talents' data REST API using Amazon API Gateway

Task 6 - Documentation
- [x] Document all API endpoint and POSTMAN testing screenshot

Task 7 - Cross Browser Testing
- [x] Auto test web app using LambdaTest and show proof (screenshot)

Task 8 - System Diagram
- [x] Draw a detailed architecture system diagram to illustrate the Saas application

Others
- [x] Authenticate user before accessing the Saas app using AWS Cognito
- [x] Create chart using Account data extraced from DynamoDB 



Developed by Jiang Lei, Franky, Lim Zi Yun, Jorin
