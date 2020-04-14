from flask import Flask
from flask_graphql import GraphQLView
from schema import schema_query

app = Flask(__name__)

# Flask Rest & Graphql Routes
# @app.route('/')
# def hello_world():
#     return 'Hello From Graphql Tutorial!'

# /
app.add_url_rule('/', view_func=GraphQLView.as_view(
    '',
    schema=schema_query, graphiql=True
))

if __name__ == '__main__':
    app.run()