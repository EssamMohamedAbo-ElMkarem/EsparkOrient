#_*_ coding:UTF-8_*_

import wolframalpha
import wikipedia
import os
import re


def get_question():
    quest_file = open(r"inner_temp\question.dx", "r", encoding="utf-8")
    question = quest_file.readline()
    quest_file.close()
    return str(question)


def get_answer():
    print(os.getcwd())
    ans_file = open(r"inner_temp\response.dx", "r", encoding="utf-8")
    answer = ans_file.readlines()
    ans_file.close()
    return str(answer)


def set_response(response):
    res_file = open(r"inner_temp\response.dx", "a", encoding="utf-8")
    res_file.write(response)
    print(response)


def main_func():
    try:
        client = wolframalpha.Client("ID") #id from your account on wolframalpha
        res = client.query(get_question())
        #image_url = json.json_result['queryresult']['pods'][0]['subpods'][0]['imagesource']  not working yet

        for pod in res.pods:
            answer = re.sub(r"[^a-zA-Z0-9 :._]", "", str("{p.title} : {p.text}".format(p=pod).encode("UTF-8")))
            set_response(answer)
    except Exception as ex_wolframalpha:
        try:
            page = wikipedia.page(get_question())
            set_response(str(page.content.encode("UTF-8")))
        except Exception as ex_wikipedia:
            set_response(ex_wikipedia)


if __name__ == "__main__":
    main_func()
