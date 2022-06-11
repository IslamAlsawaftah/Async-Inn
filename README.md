# Async-Inn

#### Islam Alsawaftah / 13-4-2022

#### Lab 17

Swagger Documentation

Install Dependency: Swashbuckle.AspnetCore

In Startup.cs, configure a new service dependency

```
public void ConfigureServices()
{
  ...
   services.AddSwaggerGen(options =>
   {
     // Make sure get the "using Statement"
     options.SwaggerDoc("v1", new OpenApiInfo()
     {
       Title = "School Demo",
       Version = "v1",
     });
   });

}
```

Create the new routes so that swagger "works"

In Startup.cs, add this to Configure()

```
app.UseSwagger( options => {
 options.RouteTemplate = "/api/{documentName}/swagger.json";
});
```
documentName is the version you gave in the previous step
Now ...

https://localhost:PORT/api/v1/swagger.json

Boom! You get a fully configured Swagger compatible JSON definition.

You can plug this directly into Swagger.io and see your live API

Even better, let's serve our own docs...
```
app.UseSwaggerUI( options => {
  options.SwaggerEndpoint("/api/v1/swagger.json", "Student Demo");
  options.RoutePrefix = "docs";
});
```

hit : http://localhost:PORT/docs is the actual documentation for your API.


http://localhost:PORT/docs is the actual documentation for your API.

#### Lab 18
 
##### Identity

Identity is the ability to add Authentication and Authorization to your web application. This includes registrations, logins, restricted access to specific members, and authentication through Facebook, Google, Twitter, etc�

ASP.NET Core Identity was created to help with the security and management of users. It provides this abstraction layer between the application and the users/role data. We can use the API in it�s entirety, or just bits and pieces as we need (such as the salting/hashing by itself) or email services. There is a lot of flexibility within ASP.NET Core Identity. We have the ability to take or leave whatever we want. Identity combines well with EFCore and SQL Server.

#### Default Identity Tables with relations

![](identity.png)

#### Register

![](register.png)

#### Login

![](login.png)


#### Some validations while registration

##### UserName validation
![](username-validation.png)

##### Password validation
![](password-validation.png)

##### Email validation
![](email-validation.png)

#### Lab 16

Add onto your current Async Inn application by cleaning up input and outputs of your controllers to be DTOs.

#### Amenity 

| Route | example data objects that get returned |
| ----------- |----------- |
| GET: api/Amenities | return all amenities |
| GET: api/Amenities/\{id} | return specific amenity |
| PUT: api/Amenities/\{id} | update specific amenity |
| POST: api/Amenities | add new amenity |
| DELETE: api/Amenities/\{id} | delete specific amenity |


#### HotelRooms 

| Route | example data objects that get returned |
| ----------- |----------- |
| GET: api/HotelRooms | return all Hotel Rooms |
| GET: api/HotelRooms/\{hotelId}/Rooms/\{roomNumber} | return all room details for a specific room |
| PUT: api/HotelRooms/\{hotelId}/Rooms/\{roomNumber} |  update the details of a specific room |
| POST: api/HotelRooms/\{hotelId}\/Rooms |  to add a room to a hotel |
| DELETE: api/HotelRooms/\{hotelId}\/Rooms/\{roomNumber} | delete a specific room from a hotel |

#### Hotel 

| Route | example data objects that get returned |
| ----------- |----------- |
| GET: api/Hotels | return all hotels |
| GET: api/Hotels/\{id} | return specific hotel |
| PUT: api/Hotels/\{id} | update specific hotel |
| POST: api/Hotels | add new hotel |
| DELETE: api/Hotels/\{id} | delete specific hotel |

#### Room 

| Route | example data objects that get returned |
| ----------- |----------- |
| GET: api/Rooms | return all rooms |
| GET: api/Rooms/\{id} | return specific room |
| PUT: api/Rooms/\{id} | update specific room |
| POST: api/Rooms | add new room |
| POST: api/Rooms/\{roomId}\/Amenity/\{amenityId} | delete specific aminity from room|
| DELETE: api/Rooms/\{id} | delete specific hotel |
| DELETE: api/Rooms/\{roomId}\/Amenity/\{amenityId} | delete specific aminity from room |

refactor the project to allow and implement dependency injection. keep the current behavior of our API server the same, and only refactoring the architecture.

Dependency Injection (DI) is a software design pattern. It allows us to develop loosely-coupled code. 

#### ERD for web based API for a local hotel asset management system.

![](ERDs.png)

#### ERD explanation

* Hotel Table: have a primary key, and it has the fields of name, city, state, address, and phone number. Relationship is (one-to-many) with the join table Hotle_Room, using Hotel primary key.

* Hotel_Room Table: have hotle_room_id as primary key , room _id,hotle_id as foreign keys,  price, pet_fiendly, room_number as payload.
 
* Room Table: have room _id as primary key used in hotel room table as foreign key , and have nickname and layout as fields. Relationship is (one to many) with join table hotel_room and (one to many) with Room_Amenities table.

* The Room Layout Table: have the fields of room_layout_id primary key, one bedroom, two bedroom, cozy studio. Relationship is (one to many) with Room table.

* Aminity Table: have amenitiy _id as primary key and aminity name as filed. Relationship (one to many) with room aminities table

* Room_Amenities Table: have aminity_id from aminity table, room_id from room table as forign keys, and room_aminity_id as composite key generated using foreign keys combined together


![](ERDs-lab12.png)

#### ERD explanation

* Hotel Table: have an int primary key, and it has the fields of name, city, state, address, and phone number as nvarchar type. Relationship is (one-to-many) with table Hotle_Room.

* HotelRoom Table: have HotelID as int  forign composite key , RoomNumber int composite key,RoomId as int foreign keys,  Rate decimal, pet_fiendly bit.
 
* Room Table: have  Id as primary key int, and have Name nvarchar and layout int as fields. Relationship is (one to many) with table HotelRoom and (one to many) with Room_Amenities table.

* The Room Layout Table: have the fields:  one bedroom, two bedroom, cozy studio. Relationship is (one to many) with Room table.

* Aminities Table: have ID as int primary key and Name nvarchar as filed. Relationship (one to many) with RoomAminities table

* RoomAmenities Table: have AminitiesID int composite forign key, RoomID int composite forign key.

Confirm in POSTMAN

![](get.png)

![](getsepec.png)

![](post.png)

![](put.png)

![](delete.png)
