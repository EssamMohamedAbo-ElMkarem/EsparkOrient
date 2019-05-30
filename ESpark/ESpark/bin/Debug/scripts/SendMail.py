def send_email(username, password, to_email, message):
    try:
        host = "smtp.gmail.com"
        port = 587
        email_conn = smtplib.SMTP(host, port)
        email_conn.ehlo()
        email_conn.starttls()
        email_conn.login(username, password)
        email_conn.sendmail(username, to_email, message)
        email_conn.close()
        set_data("success")

    except Exception as err:
        set_data("error")


def set_data(data):
    res_file = open(r"mail_data\response.dx", "a", encoding="utf-8")
    res_file.write(data)


def get_data(arg):
    data_file = open(r"mail_data/" + str(arg) + ".edxf", "r", encoding="utf-8")
    data = data_file.readline()
    data_file.close()
    return str(data)


if __name__ == "__main__":
    import smtplib
    send_email(get_data("username"), get_data("passwrd"), get_data("to_mail"), get_data("message"))
