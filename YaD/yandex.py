import json, urllib.request
from pymongo import MongoClient

#адрес для отправки json-запросов
url = 'https://api-sandbox.direct.yandex.ru/v4/json/'

#данные для OAuth-авторизации
token = 'a39e6a0f43534d06a9ca3ebc997db087'

#логин в Директе
login = 'directrf@yandex.ru'

#структура входных данных (словарь)
data = {
  "method": "GetClientsList",
  "token": "a39e6a0f43534d06a9ca3ebc997db087",
  "locale": "ru",
  "param": {
  }
}

#конвертировать словарь в JSON-формат и перекодировать в UTF-8
jdata = json.dumps(data, ensure_ascii=False).encode('utf8')

#выполнить запрос
response = urllib.request.urlopen(url, jdata)

#результаты запроса в JSON-формате
result = response.read().decode('utf-8')

#преобразование из JSON-формата в словарь
unJsonYandexResult = json.loads(result);

print(unJsonYandexResult)

client = MongoClient('mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot')

db = client.yandex_bot

collection = db.AspNetUsers

#print(collection)

#db.bar.insert({"loc": "789"})

print("-----------------------------------------\n")
#for c in db.bar.find():
#	print(db.bar.find_one({}))

#for (k,v) in unJsonYandexResult:
#	print(c["data"][0])
for key,value in unJsonYandexResult.items():
	for value2 in value:
		for key3,value3 in value2.items():
			print(key3, "->", value3)
		print("\n   +++++++++++++++++++++++++++++++   \n\n")