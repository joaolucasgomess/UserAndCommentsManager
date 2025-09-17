#!/bin/bash

dotnet ef database update --connection "User ID=app;Password=pass;Host=localhost;Port=5432;Database=users"