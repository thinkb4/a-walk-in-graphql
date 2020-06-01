from ariadne import make_executable_schema, load_schema_from_path
from ariadne.asgi import GraphQL
from resolvers import query, skill, person, eye_color
from resolvers import mutation


# import schema from GraphQL file
type_defs = load_schema_from_path("./schema.gql")

schema = make_executable_schema(
    type_defs, query, skill, person, eye_color, mutation
)
app = GraphQL(schema, debug=True)
