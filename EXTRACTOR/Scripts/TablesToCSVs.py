import tabula
import pandas as pd
import sys

path = sys.argv[1]
name = sys.argv[2]
tables = None
if len(sys.argv) == 4:
    tables = sys.argv[3]

list_of_tables = None

if tables != None:
    lista = tables.split()
    mapa = map(int, lista)
    list_of_tables = list(mapa)

df = tabula.read_pdf(path, pages = 'all')

if list_of_tables == None:
    for i in range(len(df)):
        df[i].to_csv(name+str(i)+'.csv')
else:
    for i in range(len(list_of_tables)):
        df[list_of_tables[i]].to_csv(name+str(list_of_tables[i])+'.csv')
