# ToDoList

#### By Andy Grossberg

## Description
A program using MySQL to load a todo list.

## Rules

The project for the database week of C#. Rules to follow.

## Specifications

* Load a database

* Connect to database

* Test Save and other functions.

* Build a feature to let users enter a due date when they create a new item:
  - Add date input to Form
  - Add date field to item db and item_test db
  - test date field exists
  - test date field takes input
  - Add date field code to Home Controller to enter it into the database when item is Created.
    * note that a null field should be accounted for.
    * Note that TIME cannot be trimmed out at the moment.

* Allow user to order db by date using ORDER BY  <--

* Test

* Refactor code as needed.

## Setup/Installation Requirements

* Clone the git repository from 'https://github.com/agro23/WorldData.git'.
* Run the command 'dotnet restore' to download the necessary packages.
* Run the command 'dotnet build' to build to build the app.
* Run the command 'dotnet run' to run the server on localhost.
* Use your preferred web browser to navigate to localhost:5000

## Support and contact details

* Contact the authors at andy.grossberg@gmail.com or jasun.feddema@gmail.com

## Technologies Used

* C#
* Asp .NET Core 1.1 MVC
* HTML
* CSS
* Javascript
* Bootstrap
* JQuery

### License

Copyright (c) 2018 Andy Grossberg

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
