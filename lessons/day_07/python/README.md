# [A walk in GraphQL](../../../README.md)

## Day 4 exercise - Python

Read the instructions on the [Day 4 exercise](../day_04.md#exercise) definition

### HINT (Optional):

**This patch contains an example of the code applied to createSkill, in order to reach the 3 different strategies for error handling.**

* Apply the following "hint" git patch: [error_handling.patch](error_handling.patch).

   From command line: `git apply error_handling.patch`

### Run the server

**This exercise is prepared to work with Python 3, with SQLite and SQAlchemy as ORM**

1. open a terminal
2. go to the Python exercise directory
3. run `[pip][pip3] install virtualenv` to install it. It is used to create isolated Python environments
4. run `virtualenv venv` to create the venv environment
5. Activate virtual environment:

   * On Windows run `venv\Scripts\activate.bat`  (*`deactivate.bat` to deactivate the environment*).
   * On Linux run `source env/bin/activate`      (*`deactivate` to deactivate the environment*)
6. run `[pip][pip3] install -r requirements.txt` to install the needes packages if you didn't before
7. run `uvicorn app:app` to start the GraphQL server (uvicorn [file_name}:[app_name])
8. open your browser and type `http://localhost:8000/` to display the GraphQL playground so you can run the queries against the server

### The project

* SQLite Data source (../datasource/data.db)
* [Server app](app.py)
* [Schemas](schema/)
* [Resolver map](resolvers.py)
* [Models](models.py)
* [db abstraction](data.py)
