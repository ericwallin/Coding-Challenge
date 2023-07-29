# Response to Coding Challenge

# Release Notes
* Made the Contact page the default page that loads
* Added API Controller /api/contacts accepting 2 parameters
    * lastName
    * sortAscending [true/false]
* Updated navigation to have a convenient link to the api method, including query string parameters for ease of use
* Used Sqlite as data store, needed a ninja move to make the Sqlite in-memory database stay persistent between requests, but not between restarts.
* Updated .gitignore, was outdated and adding obj and bin files

# TODO:
* Make logging more useful
* Add instrumentation
* Add security / do security review
* Upgrade to .Net 6
* Containerize

**The Problem**: Design a web based mailing list application. The following requirements are relatively simple, but the application should be designed so that it could be used as the basis for a more complete implementation. In other words, use sound design patterns and coding practices.
Requirements:
1.	:white_check_mark: Provide a simple html page in which a user can enter the following. The page merely needs to be functional and does not need to be styled in any way.
    *	Last Name
    *	First Name
    *	Email
2.	:white_check_mark: When the user submits this data, the input should be validated against the following rules. Any validation errors should be presented to the user.
    *	First Name cannot be empty
    *	Last Name cannot be empty
    *	Email must be a valid email address.
    * If all validation succeeds, a confirmation page should be displayed which tells the user the data was received by the system.
3.	:white_check_mark: All data submitted in this way must be saved, but only for the duration of the application’s run. It does not need to survive a restart of the application. However, in your design consider that this application will ultimately have to save this data to a persistent datastore.
4.	:white_check_mark: A REST endpoint must be provided to retrieve all mailing list entries. **/api/contacts?lastName=smith&sortAscending=true** This endpoint should take 2 optional parameters:
5.	:white_check_mark: Last name- if specified, only records with this last name are returned **requires exact matching, case insensitive**
6.	:white_check_mark: Ascending/Descending flag which indicates how to sort records. If not specified, default behavior is to sort ascending. Records should be sorted by last name. Where last names are equal, records should be sorted by first name. :paperclip: **I played the role of Product Engineer and made the executive decision to make this case insensitive as a usability enhancement.** 
7.	:white_check_mark: At least one automated test must be provided which tests one of your .net components. **Includes 2 business logic tests and 1 api controller test.**
8.  :white_check_mark: Security is not required for this exercise. :no_entry_sign: :closed_lock_with_key: :see_no_evil:

Optional:

1.	:star: Add additional tests **3 vs. the 1 that was required**
2.	:star: Implement appropriate design patterns or principles such as dependency injection **Used dependency injection for business logic and data access. Data access layer using IContactsRepostiory is implemented as InMemoryContactsRepository using an in-memory Sqlite database using Dapper ORM. Another implementation using a persistent database could easily be done.**

:shipit:

