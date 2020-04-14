# [A walk in GraphQL](/README.md)

## Day 1 exercise - Python

Read the instructions on the [Day 1 exercise](../day_01.md#exercise) definition

### Run the server

1. open a terminal
2. go to the Python exercise directory
3. run `pip install virtualenv` to install it. It is used to create isolated Python environments
4. run `virtualenv venv` to create the venv environment
5. Activate virtual environment:
   - On Windows run `venv\Scripts\activate.bat`  (*`deactivate.bat` to deactivate the environment*).
   - On Linux run `source env/bin/activate`      (*`deactivate` to deactivate the environment*) 
6. run `pip install -r requirements.txt` to install the needes packages if you didn't before
7. run `python app.py` to start the GraphQL server
8. open your browser and type `http://localhost:5000/` to display the GraphQL playground so you can run the queries against the server

### The project

- [Data source](../datasource/data.json)
- [Server app](app.py)
- [Schema and Top level resolver](schema.py)
- [Skill model with Field level resolvers](skill.py)
- [db abstraction](data.py)
