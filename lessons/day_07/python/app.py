from ariadne import make_executable_schema, load_schema_from_path
from ariadne.asgi import GraphQL
from resolvers import query, skill, eye_color, mutation, person, global_search
from pathlib import Path


working_dir = Path.cwd()

# import schema from GraphQL file
type_def_global_search = load_schema_from_path(working_dir / "schema/globalSearch.gql")
type_def_person = load_schema_from_path(working_dir / "schema/person.gql")
type_def_skill = load_schema_from_path(working_dir / "schema/skill.gql")
type_def_schema = load_schema_from_path(working_dir / "schema/schema.gql")

schema = make_executable_schema(
    [type_def_global_search, type_def_person, type_def_skill, type_def_schema],
    query, skill, eye_color, mutation, person, global_search
)
app = GraphQL(schema)
