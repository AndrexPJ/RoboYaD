import json, urllib.request, time
from pymongo import MongoClient
from datetime import datetime

url = 'https://api-sandbox.direct.yandex.ru/v4/json/'

data = {
	"method": "GetClientsList",
	"token": "a39e6a0f43534d06a9ca3ebc997db087",
	"locale": "ru",
	"param": {}
}
jdata = json.dumps(data, ensure_ascii=False).encode('utf8')
response = urllib.request.urlopen(url, jdata)
result = response.read().decode('utf-8')
unJsonYandexResult = json.loads(result);

logins = []
for key,clients in unJsonYandexResult.items():
	for client in clients:
		logins.append(client["Login"])


data = {
	"method": "GetCampaignsList",
	"token": "a39e6a0f43534d06a9ca3ebc997db087",
	"locale": "ru",
	"param": logins
}
jdata = json.dumps(data, ensure_ascii=False).encode('utf8')
response = urllib.request.urlopen(url, jdata)
result = response.read().decode('utf-8')
unJsonYandexResult = json.loads(result);

campaignIDs = []
for key,campaigns in unJsonYandexResult.items():
	for campaign in campaigns:
		campaignIDs.append(campaign["CampaignID"])
		

data = {
	"method": "GetBanners",
	"token": "a39e6a0f43534d06a9ca3ebc997db087",
	"locale": "ru",
	"param": {
		"CampaignIDS": campaignIDs
	}
}
jdata = json.dumps(data, ensure_ascii=False).encode('utf8')
response = urllib.request.urlopen(url, jdata)
result = response.read().decode('utf-8')
unJsonYandexResult = json.loads(result);

BannerIDs=[]
for key,banners in unJsonYandexResult.items():
	for banner in banners:
		BannerIDs.append(banner["BannerID"])
			
			
client = MongoClient('mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot')
db = client.yandex_bot

while 1:
	data = {
		"method": "GetBannerPhrases",
		"token": "a39e6a0f43534d06a9ca3ebc997db087",
		"locale": "ru",
		"param": BannerIDs
	}
	jdata = json.dumps(data, ensure_ascii=False).encode('utf8')
	response = urllib.request.urlopen(url, jdata)
	result = response.read().decode('utf-8')
	unJsonYandexResult = json.loads(result);
	

	for key,banners in unJsonYandexResult.items():
		for banner in banners:
			db.phrases.insert({
				"PhraseID": banner["PhraseID"],
				"Min": banner["Min"],
				"Max": banner["Max"],
				"PremiumMin": banner["PremiumMin"],
				"PremiumMax": banner["PremiumMax"],
				"datetime": datetime.now()})
	
	print(datetime.strftime(datetime.now(), "%Y.%m.%d %H:%M:%S"))
	time.sleep(300)