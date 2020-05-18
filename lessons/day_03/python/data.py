import os
from flata import Flata

filename = os.path.abspath("../datasource/data.json")
db = Flata(filename)
