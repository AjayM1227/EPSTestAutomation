Feature: UserLoginFeature
	

#Purpose:Verifying the login functionality for EPS admin with valid credentials
Scenario: Login as EPSAdminUser with valid user name and valid password
Given I access the URL as "EPSAdminUser01"
Then I should be displayed with "Log in to your accoun" login page
When I login as "EPSAdminUser01" with "Valid username" and "Valid password"

#Purpose:Verifying the login functionality for EPS admin with valid credentials
Scenario: Login as EPSSuperAdminUser with valid user name and valid password
Given I access the URL as "EPSSuperAdminUser01"
Then I should be displayed with "Log in to your account" login page
When I login as "EPSSuperAdminUser01" with "Valid username" and "Valid password"



Scenario: Logout as user
When I click on "LogOut" option in User Profile
