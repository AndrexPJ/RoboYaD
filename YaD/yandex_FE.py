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
	"method": "GetCampaignsList",
	"token": "a39e6a0f43534d06a9ca3ebc997db087",
	"locale": "ru",
	"param": ["sbx-directNJpeiz", "sbx-directytWZSc", "sbx-directDuljAW"]
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
	
	#GetClientsList
	for key,value in unJsonYandexResult.items():
		for value2 in value:
			db.companies.insert({
				"ClientLogin": value2["Login"],
				"CampaignID": value2["CampaignID"],
				"Name": value2["Name"],
				"Sum": value2["Sum"],
				"Rest": value2["Rest"],
				"Shows": value2["Shows"],
				"Clicks": value2["Clicks"],
				"Status": value2["Status"],
				"datetime": datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S")})
	
	#GetClientsList
	#for key,value in unJsonYandexResult.items():
	#	for value2 in value:
	#		for key3,value3 in value2.items():
	#			print(key3, "->", value3)
	#		print("\n","+++++++++++++++++++++++++","\n\n")		
			
	#		db.prices.insert({"Min": value2["Min"], "Max": value2["Max"],
	#		"PremiumMin": value2["PremiumMin"], "PremiumMax": value2["PremiumMax"],
	#		"Phrase": value2["Phrase"], 
	#		"datetime": datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S")})
			
	print(datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S"))
	time.sleep(10)