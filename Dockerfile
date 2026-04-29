FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sadece proje dosyalarını değil, tüm CleanArchitecture klasörünü kopyala
# Bu sayede tek tek dosya yolu yazma zahmetinden ve hatalardan kurtuluruz
COPY ["CleanArchitecture/", "CleanArchitecture/"]

# Restore işlemini solution üzerinden veya WebApi üzerinden yap
RUN dotnet restore "CleanArchitecture/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj"

# Geri kalan her şeyi kopyala
COPY . .

WORKDIR "/src/CleanArchitecture/CleanArchitecture.WebApi"
RUN dotnet publish "CleanArchitecture.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "CleanArchitecture.WebApi.dll"]