import tabula
import pandas as pd
import sys

path = sys.argv[1]
name = sys.argv[2]
tables = sys.argv[3]
list_of_tables = None

if not tables:
    lista = tables.split()
    mapa = map(int, lista)
    list_of_tables = list(mapa)

df = tabula.read_pdf(path, pages = 'all')

writer = pd.ExcelWriter(path+'.xlsx', engine='xlsxwriter')

if list_of_tables == None:
    for i in range(len(df)):
        df[i].to_excel(writer, sheet_name='Sheet'+str(i))

else:
    for i in range(len(list_of_tables)):
        df[list_of_tables[i]].to_excel(writer, sheet_name='Sheet'+str(list_of_tables[i]))

writer.save()