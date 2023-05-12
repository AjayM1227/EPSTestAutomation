Feature: UsersManagementFeature
This feature file ardontains all the scenarios related to user management

Scenario:Creating User with LTA Contracts AO role as EPSAdminUser
Given I access the URL as "EPSAdminUser01"
Then I should be displayed with "Log in to your account" login page
When I login as "EPSAdminUser01" with "Valid username" and "Valid password"
When I Click on the "User Access Management" option in "Settings"
And I click on "ADD USER" button in User Access Management
And I enter user details of "NewEPSUser01" in "OPS" group with "LTA Contracts AO"
When I click on "LogOut" option in User Profile
