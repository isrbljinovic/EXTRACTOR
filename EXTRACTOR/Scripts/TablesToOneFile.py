import tabula
import pandas as pd
import sys

path = sys.argv[1]
name = sys.argv[2]

df = tabula.read_pdf(path, pages = 'all')

writer = pd.ExcelWriter(path+'.xlsx', engine='xlsxwriter')

for i in range(len(df)):
    df[i].to_excel(writer, sheet_name='Sheet'+str(i))

writer.save()