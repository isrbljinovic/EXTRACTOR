import tabula
import pandas as pd
df = tabula.read_pdf('C:/Users/Ivan/Desktop/PDF/Table.pdf', pages = 'all')

df[0].to_excel('file_'+str(16)+'.xlsx')
df[0].to_csv('file_'+str(16)+'.csv')


for i in range(len(df)):
 df[i].to_excel('file_'+str(i)+'.xlsx')