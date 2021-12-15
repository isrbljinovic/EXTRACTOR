import tabula
import pandas as pd

df = tabula.read_pdf('Table.pdf', pages = 'all')

for i in range(len(df)):
    df[i].to_csv('file_'+str(i)+'.csv')
