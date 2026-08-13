# RosteringApplication

A simple MVVM application that can be used for rostering workers and keeping track of their shifts.

Currently the program has features for:
- Adding employees to the program
- Adding shifts for these employees
- Persistent saving of shifts to locally stored Json files

I am looking to add:
- Deletion and modification of employees and shifts (and saving in Json)
- Implementation of overnight shifts
- Adding breaks to shifts
- Warnings about ongoing shifts
- More streamlined UI

## Why Json?
While using a query-able database such as MongoDB or SQL would be very realistic for a scenario like this, I believe that, in the meantime, I can show basic CRUD and schema creation via locally stored Json files, as they are more lightweight for this situation. 
