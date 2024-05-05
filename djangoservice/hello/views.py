from django.shortcuts import render

# hello/views.py

from django.http import HttpResponse, JsonResponse
import requests


def third_party(request, count=0):
    # Call third-party service
    response = requests.get("https://open.er-api.com/v6/latest/USD")

    # Check if request was successful
    if response.status_code == 200:
        # Return response from third-party service
        data = response.json()

        # Extract rates from the response
        rates = data.get('rates', {})

        # Sort the rates by their values
        sorted_rates = dict(
            sorted(rates.items(), key=lambda item: item[1], reverse=True))

        # Determine the number of rates to return
        if count == 0:
            # If no count is provided, return all rates
            selected_rates = sorted_rates
        else:
            # If count is provided, return the top N rates
            selected_rates = dict(list(sorted_rates.items())[:count])

        # Return the selected rates
        return JsonResponse({"message": selected_rates})
    else:
        # If request was not successful, return error status code
        return JsonResponse({"error": "Failed to fetch data from third-party service"}, status=500)


def hello_view(request):
    return JsonResponse({"message": "Hello from Microservice 3!"})
