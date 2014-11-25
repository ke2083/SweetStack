Feature: CreateAccount
	As a user, I want to be able to register for an account so that I can use SweetStack.

@mytag
Scenario: Create account
	Given I have an account service
	When I create an account
	Then the account should be created

Scenario: Create account with missing name
	Given I have an account service
	When I create an account with a missing name
	Then the account should not be created
