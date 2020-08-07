# [A walk in GraphQL](../README.md)

## Python setup

Install or update [Python](https://www.python.org/downloads/) version >=3.6

*`Pip` is also needed (comes with Python installation)*

Links (*just in case*):

- In [Windows](https://www.journaldev.com/30076/install-python-windows-10)
- In [Linux](https://phoenixnap.com/kb/how-to-install-python-3-ubuntu)

### Run the server

1. open a terminal
2. go to the Python exercise directory
3. run `[pip][pip3] install virtualenv` to install it. It is used to create isolated Python environments
4. run `virtualenv venv` to create the venv environment
5. Activate virtual environment:
   - On **Windows** run `venv\Scripts\activate.bat`  (*`deactivate.bat` to deactivate the environment*).
   - On **Linux** run `source env/bin/activate`      (*`deactivate` to deactivate the environment*)
6. run `[pip][pip3] install -r requirements.txt` to install the needes packages if you didn't before
7. run `uvicorn app:app` to start the GraphQL server (uvicorn [file_name}:[app_name])

### GraphQL Playground

Open your browser and type [http://localhost:8000/](http://localhost:8000/) to display the GraphQL playground so you can run the queries against the server
