# official .NET sdk 8 (DEVELOPMENT)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# container work directory
WORKDIR /app

# copy files into container
COPY . .

# publish
RUN dotnet publish -c Release -o /app/publish

# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# copy from build
COPY --from=build /app/publish .

# download litestream
ADD https://github.com/benbjohnson/litestream/releases/download/v0.3.13/litestream-v0.3.13-linux-amd64.tar.gz /tmp/litestream.tar.gz
RUN tar -C /usr/local/bin -xzf /tmp/litestream.tar.gz

# configure litestream
COPY litestream.yml /etc/litestream.yml

# copy db
COPY hikingblog.db .


# expose port 8080
EXPOSE 8080

# startup script
COPY run.sh /app/run.sh
RUN chmod +x /app/run.sh

# set entry point
ENTRYPOINT ["/app/run.sh"]