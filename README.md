# Noba Car rental
This is Fictional car rental service that allows rental companies to receive rental bookings, manage the bookings and bill customers.
The main scope of this implementation is around two use cases
- Registering a pickup : A customer coming in to pick up a rental car 
- Registering a dropoff: A custumer rreturning a rental car at the end of the rental period.

## Execution plan

##### Goals: 
- Design a flexible solution and implement the 2 use cases. 
- Add unit tests to ensure that the use cases function as expected. 
- Add simple UI for demo

#### Ambiguities

- Does the presence of a booking number mean that a booking is made before a rental starts?
- Can a customer book a rental only on the premises?
- Will the structure of the price calculation change often?
- Are the time booked always the same as the final rental?
- Are customers billed only for the time you booked?
- Are we rounding up/down days when billing?

### Assumptions

- Currency of operation is SEK
- Customers can book both in person(in advance) or via walk-in
-The times booked at the rental period can be different. This covers cases of early pickup or late drop-off.
- The person that made a booking is he same person that picks up the car.
- Days are rounded up when calculating
