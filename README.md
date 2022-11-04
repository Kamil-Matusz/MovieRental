# MovieRental
Project representing a movie rental shop with order handling made in .NET MVC 

# Requirements
`` SQL Server 2016 ``
<br/>
`` .Net Core 5``
<br/>

# Theme
``Solar Theme``
<br/>
Download form: https://bootswatch.com/
<br/>

# Graphic diagram of database
![](/git/database-diagram.png)

# Default login
### Admin
`` Login : admin@admin.com``
<br/>
`` Password : admin``
<br/>

### Admin functions
- Adding movies
- Deleting movies
- Editing movies
- Orders handling
- Receiving emails from users

### User
``Login : user@user.com``
<br/>
`` Password : user``
<br/>
### User functions
 - Viewing the list of movies
 - Searching movies after the title
 - Placing orders
 - Creating new account and logging for on this account
 - Sending an email with request

# Database
### Configuration
To ensure that error dosen't occur when connecting to database, data needed for connection should be substituted in the file ``appsettings.Development.json`` by line ``3``
