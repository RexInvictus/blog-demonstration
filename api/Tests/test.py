import requests
import threading


def one():
    for i in range(1000):
        response_get_blog = requests.get("http://localhost:8080/api/blog")
        print("GET /api/blog - Status Code:", response_get_blog.status_code)
        print("GET /api/blog - Response:", response_get_blog.text)
def two():
    for i in range(100):
        response_get_blog = requests.get("http://localhost:8080/api/blog/2")
        print("GET /api/blog - Status Code:", response_get_blog.status_code)
        print("GET /api/blog - Response:", response_get_blog.text)

thread = threading.Thread(target=one)
thread2 = threading.Thread(target=two)

thread.start()
thread2.start()

thread.join()
thread2.join()

print("Done")