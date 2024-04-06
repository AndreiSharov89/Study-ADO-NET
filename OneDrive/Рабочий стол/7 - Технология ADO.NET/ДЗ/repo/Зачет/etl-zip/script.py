import csv
import pandas as pd

COUNTRIES_INPUT = 'countries.csv'
CSV_INPUT = 'allCountriesCSV.csv'
CSV_INPUT2 = 'allCountriesCSV2.csv'
COUNTRIES_OUTPUT = 'countries_out.csv'
#ZIPCOORDS_OUTPUT = 'zipCoords.csv'
#ZIPADDRESSES_OUTPUT = 'zipAddresses.csv'
ZIPCOORDS_OUTPUT2 = 'zipCoords2.csv'
ZIPADDRESSES_OUTPUT2 = 'zipAddresses2.csv'



def isfloat(num):
    try:
        float(num)
        return True
    except ValueError:
        return False
def isint(num):
    try:
        int(num)
        return True
    except ValueError:
        return False


inp = pd.read_csv(CSV_INPUT, low_memory=False)
inp.head()
inp.columns
#print(inp.head())
#print (inp.columns)
result = inp.drop_duplicates(subset = ['COUNTRY', 'POSTAL_CODE'], keep='first')
result.to_csv(CSV_INPUT2, encoding='utf-8')

country_abbr = {}
with open(COUNTRIES_INPUT, 'r') as file:
    csv_reader = csv.DictReader(file)
    country_data = [row for row in csv_reader]
for country in country_data:
    country_abbr[country['CODE']] = country['FULL_NAME']
#print(country_abbr)
countries_list = list(country_abbr.keys())
#print(countries_list)
countries_dict = {countries_list[idx]: {'country_id': idx, 'country_code':
                   countries_list[idx], 'country_name': country_abbr[countries_list[idx]]} for idx in range(len(countries_list))}
#print(countries_dict)

countries_list2 = list(countries_dict.values())
#print(countries_list2[0].keys())
with open(COUNTRIES_OUTPUT, 'w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=countries_list2[0].keys())
    writer.writeheader()
    for row in countries_list2:
        writer.writerow(row)

with open(CSV_INPUT, 'r', encoding='utf-8') as file:
    csv_reader = csv.DictReader(file)
    zip_list = [row for row in csv_reader]
with open(CSV_INPUT2, 'r', encoding='utf-8') as file:
    csv_reader = csv.DictReader(file)
    zip_list2 = [row for row in csv_reader]
# print(zip_list[100003])
# category_names_list = list(market_list[0].keys())[28:-1]
# categories_dict = {category_names_list[idx]: {'category_id': idx, 'category':
#                   category_names_list[idx]} for idx in range(len(category_names_list))}
# print(categories_dict)

address_dict = {}
ctr_zip = 0
coords_dict = {}
#ctr_coords = 0
# markets_dict = {}
# ctr_markets = 0
# ctr_markets_categories = 0
# markets_categories_list = []
#print(countries_list)

'''
for zip in zip_list:
    zip['COUNTRY'] = zip['COUNTRY'].strip()
    zip['POSTAL_CODE'] = zip['POSTAL_CODE'].strip()
    if zip['POSTAL_CODE'] != '' and zip['COUNTRY'] != '' and zip['POSTAL_CODE'] not in coords_dict:
        coords_dict[ctr_zip] = {'zip_id': ctr_zip,
                                  'country_id': countries_dict[zip['COUNTRY']]['country_id'],
                                  'zip_code': zip['POSTAL_CODE'].strip(),
                                  'lat': float(zip['LATITUDE']) if isfloat(zip['LATITUDE']) else None,
                                  'lon': float(zip['LONGITUDE']) if isfloat(zip['LONGITUDE']) else None,
                                  'accuracy': int(zip['ACCURACY']) if isint(zip['ACCURACY'])else 0}
        address_dict[ctr_zip] = {'zip_id': ctr_zip,
                              'country_id': countries_dict[zip['COUNTRY']]['country_id'],
                              'zip_code': zip['POSTAL_CODE'].strip(),
                              'city': zip['CITY'].strip() if zip['CITY'].strip() != '' else None,
                              'state': zip['STATE'].strip() if zip['STATE'].strip() != '' else None,
                              'county': zip['COUNTY'].strip() if zip['COUNTY'].strip() != '' else None,
                              'community': zip['COMMUNITY'].strip() if zip['COMMUNITY'].strip() != '' else None}
        ctr_zip += 1

address_list = list(address_dict.values())
with open(ZIPADDRESSES_OUTPUT, 'w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=address_list[0].keys())
    writer.writeheader()
    for row in address_list:
        writer.writerow(row)

coords_list = list(coords_dict.values())
with open(ZIPCOORDS_OUTPUT, 'w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=coords_list[0].keys())
    writer.writeheader()
    for row in coords_list:
        writer.writerow(row)
'''
address_dict = {}
ctr_zip = 0
coords_dict = {}

for zip in zip_list2:
    zip['COUNTRY'] = zip['COUNTRY'].strip()
    zip['POSTAL_CODE'] = zip['POSTAL_CODE'].strip()
    if zip['POSTAL_CODE'] != '' and zip['COUNTRY'] != '' and zip['POSTAL_CODE'] not in coords_dict:
        coords_dict[ctr_zip] = {'zip_id': ctr_zip,
                                  'country_id': countries_dict[zip['COUNTRY']]['country_id'],
                                  'zip_code': zip['POSTAL_CODE'].strip(),
                                  'lat': float(zip['LATITUDE']) if isfloat(zip['LATITUDE']) else None,
                                  'lon': float(zip['LONGITUDE']) if isfloat(zip['LONGITUDE']) else None,
                                  'accuracy': int(zip['ACCURACY']) if isint(zip['ACCURACY'])else 0}
        address_dict[ctr_zip] = {'zip_id': ctr_zip,
                              'country_id': countries_dict[zip['COUNTRY']]['country_id'],
                              'zip_code': zip['POSTAL_CODE'].strip(),
                              'city': zip['CITY'].strip() if zip['CITY'].strip() != '' else None,
                              'state': zip['STATE'].strip() if zip['STATE'].strip() != '' else None,
                              'county': zip['COUNTY'].strip() if zip['COUNTY'].strip() != '' else None,
                              'community': zip['COMMUNITY'].strip() if zip['COMMUNITY'].strip() != '' else None}
        ctr_zip += 1



address_list = list(address_dict.values())
with open(ZIPADDRESSES_OUTPUT2, 'w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=address_list[0].keys())
    writer.writeheader()
    for row in address_list:
        writer.writerow(row)

coords_list = list(coords_dict.values())
with open(ZIPCOORDS_OUTPUT2, 'w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=coords_list[0].keys())
    writer.writeheader()
    for row in coords_list:
        writer.writerow(row)

#print(address_dict[700000])
#print(coords_dict[700000])
#print(countries_dict.keys())
#print(list(address_dict[0].keys()))'''


