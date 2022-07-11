ARG buildconfiguration=Release
ARG buildruntime=linux-musl-x64
ARG framework=net6.0

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS sdk

FROM sdk AS publish
WORKDIR /app/src
COPY ./ /app/src/
ARG buildconfiguration
ARG buildruntime
ARG framework
RUN dotnet publish MissingAadProvider/MissingAadProvider.csproj \
    --output ./output/api/ \
    --configuration $buildconfiguration \
    --runtime $buildruntime \
    --framework $framework \
    --self-contained

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS run
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false LC_ALL=en_GB.UTF-8 LANG=en_GB.UTF-8

FROM run
WORKDIR /app/api
COPY --from=publish /app/src/output/api/ ./
ENTRYPOINT ["./MissingAadProvider"]