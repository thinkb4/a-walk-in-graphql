from ariadne import make_executable_schema, load_schema_from_path
from ariadne.asgi import GraphQL
from resolvers import skill, query

# import schema from GraphQL file
type_defs = load_schema_from_path("./schema.gql")

schema = make_executable_schema(type_defs, query, skill)
app = GraphQL(schema, debug=True)
