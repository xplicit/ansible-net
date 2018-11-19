#!/bin/sh

FrameworkPathOverride=/usr/lib/mono/4.5/ dotnet pack ansible-net.sln -c Release
