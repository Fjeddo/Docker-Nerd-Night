## Den h�r versionen av l�sningen ska k�ras med Linux Containers ist�llet f�r Windows Containers.

docker-compose pekar ut:
- pubsub: en redis-server �tkomlig via redis-cli p� 127.0.0.1:32999. I kod �tkomlig via 'pubsub'
- a.webapi: ett ASP.NET Core Web Api, f�r att posta rapporter till a.website. Detta kan g�ras genom pubsub eller direkt via http. Titta p� ReportsController f�r att routes
- a.website: en ASP.NET Core Mvc WebApp, signalR-enabled som loggar till console (F12 f�r dev tools). Lyssnar p� pubsub via 'pubsub', se StartAsync i RedisSubscriptionService. Inneh�ller �ven en route f�r a.webapi att konsumera f�r att inte g� via redis pubsub.

**a.webapi** "surfbar" via http://localhost:32980/index.html
**a.website** "surfbar" via http://localhost:32880/index.html
**pubsub** loggas p� via tex '.\redis-cli.exe -p 32999''