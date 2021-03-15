

Application
This is where business Logic lies

Validation
Migrator Middleware for API
Custon Rest Exeption Handling Middleware
Login Handler





Persistance  DataBase Layer
Migration
Datacontext


API
Custom ErrorHandlingMiddleware // refer this to startup too
Nuget ASPnetcore.Identity.Ui


Domain : Should not dependent on any dependncy , but for this instace using Asp.net identity core dependency



________________________________________________________________________________

					Http Request
				API ----------------------->   Client
				 |  <----------------------
				 |      HTTP request
				 |
			 	 V
Persistance <------------    Application
	|		       	 |
	|			 |
	|			 V
	--------------->      Domain

________________________________________________________________________________