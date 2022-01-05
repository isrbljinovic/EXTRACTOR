import tabula
import pandas as pd
import sys

path = sys.argv[1]
name = sys.argv[2]
tables = list()

if len(sys.argv) >= 4:
    for i in range(3, len(sys.argv)):
        tables.append(int(sys.argv[i]) - 1)

df = tabula.read_pdf(path, pages = 'all')

if len(tables) == 0:
    for i in range(len(df)):
        df[i].to_excel(name+str(i)+'.xlsx')
else:
    for i in range(len(tables)):
        df[tables[i]].to_excel(name+str((tables[i]+1))+'.xlsx')

