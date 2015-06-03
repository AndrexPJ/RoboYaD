import json, urllib.request, time
from pymongo import MongoClient
from datetime import datetime

client = MongoClient('mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot')

db = client.yandex_bot

#адрес для отправки json-запросов
url = 'https://api-sandbox.direct.yandex.ru/v4/json/'

#данные для OAuth-авторизации
token = 'a39e6a0f43534d06a9ca3ebc997db087'

#логин в Директе
login = 'directrf@yandex.ru'

#структура входных данных (словарь)
data = {
  "method": "GetBannerPhrases",
  "token": "a39e6a0f43534d06a9ca3ebc997db087",
  "locale": "ru",
  "param": [
		1008398, 
		1008399,
		1008400
	]
}
#конвертировать словарь в JSON-формат и перекодировать в UTF-8
jdata = json.dumps(data, ensure_ascii=False).encode('utf8')

while 1:
	#выполнить запрос
	response = urllib.request.urlopen(url, jdata)

	#результаты запроса в JSON-формате
	result = response.read().decode('utf-8')

	#преобразование из JSON-формата в словарь
	unJsonYandexResult = json.loads(result);

	#print(unJsonYandexResult)

	#print(collection)

	#db.bar.insert({"loc": "789"})

	#for c in db.bar.find():
	#	print(db.bar.find_one({}))

	#for (k,v) in unJsonYandexResult:
	#	print(c["data"][0])
	for key,value in unJsonYandexResult.items():
		for value2 in value:
			db.prices.insert({"Min": value2["Min"], "Max": value2["Max"],
			"PremiumMin": value2["PremiumMin"], "PremiumMax": value2["PremiumMax"],
			"Phrase": value2["Phrase"], 
			"datetime": datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S")})
			
	print(datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S"))
	time.sleep(10)