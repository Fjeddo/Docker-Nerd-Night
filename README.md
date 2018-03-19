## Den här versionen av lösningen ska köras med Linux Containers istället för Windows Containers.

docker-compose pekar ut:
- pubsub: en redis-server åtkomlig via redis-cli på 127.0.0.1:32999. I kod åtkomlig via 'pubsub'
- a.webapi: ett ASP.NET Core Web Api, för att posta rapporter till a.website. Detta kan göras genom pubsub eller direkt via http. Titta på ReportsController för att routes
- a.website: en ASP.NET Core Mvc WebApp, signalR-enabled som loggar till console (F12 för dev tools). Lyssnar på pubsub via 'pubsub', se StartAsync i RedisSubscriptionService. Innehåller även en route för a.webapi att konsumera för att inte gå via redis pubsub.

**a.webapi** "surfbar" via http://localhost:32980/index.html

**a.website** "surfbar" via http://localhost:32880/index.html

**pubsub** loggas på via tex '.\redis-cli.exe -p 32999''
