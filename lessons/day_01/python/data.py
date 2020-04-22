import os
from flata import Flata
from flata.storages import JSONStorage

filename = os.path.abspath("../datasource/data.json")
db = Flata(filename, storage=JSONStorage)
