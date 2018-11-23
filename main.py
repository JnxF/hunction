import requests
import json
import matplotlib.pyplot as plt

base_url = "https://v0wwaqqnpa.execute-api.eu-west-1.amazonaws.com/V1"
api_key = "8zwOJ3DtNS5QHhs8IKL259i0vjI1OB284zjKAGvr"
headers = {
    'accept': 'application/json',
    'x-api-key': api_key
}

def parseJSON(request):
    return json.loads(request.text)


def main():
    times = '''
        {
          "timeDateFrom" : "2018-10-10T11:25:00.589234Z",
          "timeDateTo" : "2018-10-10T12:00:00.589234Z"
        }
    '''

    data = parseJSON(requests.post(base_url + "/sites/site_exp/thermalimage", headers=headers, data=times))['data']['items']

    img = data[0]['image']

    print(len(img[2]))

    import numpy as np
    print(np.matrix(img))

    plt.imshow(img, cmap='cool', annot=True, interpolation='nearest', fmt="d")
    plt.show()


if __name__ == '__main__':
    main()
