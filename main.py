import requests
import json
import matplotlib.pyplot as plt
import time

base_url = "https://v0wwaqqnpa.execute-api.eu-west-1.amazonaws.com/V1"
api_key = "8zwOJ3DtNS5QHhs8IKL259i0vjI1OB284zjKAGvr"
headers = {
    'accept': 'application/json',
    'x-api-key': api_key
}

def parseJSON(request):
    return json.loads(request.text)


def main():

    inf = '''
    {
  "timeDateFrom" : "2018-10-10T11:25:00.589234Z",
  "timeDateTo" : "2018-10-10T12:00:00.589234Z"
}'''

    data = requests.post(base_url + "/sites/site_exp/thermalimage", headers = headers, data = inf)
    data = parseJSON(data)["data"]["items"]


    for t in data:
        x = t['image']
        plt.imshow(x, interpolation='nearest')
        plt.show()


if __name__ == '__main__':
    main()
