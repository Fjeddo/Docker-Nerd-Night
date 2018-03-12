FROM microsoft/nanoserver

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN $newPath = ('C:\Redis;{0}' -f $env:PATH); \
	Write-Host ('Updating PATH: {0}' -f $newPath); \
	setx /M PATH $newPath;

ENV REDIS_VERSION 3.2.100
ENV REDIS_DOWNLOAD_URL https://github.com/MicrosoftArchive/redis/releases/download/win-3.2.100/Redis-x64-3.2.100.zip

RUN Write-Host ('Downloading {0} ...' -f $env:REDIS_DOWNLOAD_URL); \
	Invoke-WebRequest -Uri $env:REDIS_DOWNLOAD_URL -OutFile 'redis.zip'; \
	\
	Write-Host 'Expanding ...'; \
	Expand-Archive redis.zip -DestinationPath C:\Redis; \
	\
	Write-Host 'Verifying install ("redis-server --version") ...'; \
	redis-server --version; \
	\
	Write-Host 'Removing ...'; \
	Remove-Item redis.zip -Force

VOLUME C:\\data
WORKDIR C:\\data

EXPOSE 6379
CMD ["redis-server.exe", "--protected-mode no", "--appendonly yes"]