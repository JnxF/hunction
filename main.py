import requests

base_url = "https://v0wwaqqnpa.execute-api.eu-west-1.amazonaws.com/V1"
api_key = "8zwOJ3DtNS5QHhs8IKL259i0vjI1OB284zjKAGvr"
headers = {
    'accept': 'application/json',
    'x-api-key': api_key
}

def get(url):
    return requests.get(base_url + url, headers = headers)

def post(url):
    return requests.post(base_url + url, headers = headers)

def put(url):
    return requests.put(base_url + url, headers = headers)

def delete(url):
    return requests.delete(base_url + url)



def main():
    print(get("/sites/site_exp").text)


if __name__ == '__main__':
    main()
