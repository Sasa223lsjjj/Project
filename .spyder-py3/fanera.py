import cv2
from scipy.ndimage import measurements
import os.path
from PIL import Image
import glob

# Проверка существования файла при открытии
def open_check_files(name_image):
    check_file = os.path.exists(name_image)
    if check_file == True:
        return False
    else:
        print("     Такого файла не существует!")
        return True

def main(filename: str):
    # Открытие изображения
    img_color = cv2.imread(filename, cv2.IMREAD_COLOR)
    # Работаем с зеленым каналом изображения, так как он наиболее резко изменяется на сучках
    img = img_color[:, :, 1]
    # Размытие изображения методом Гаусса по вертикальной оси, чтобы максимально снизить влияние темных волокон
    img = cv2.GaussianBlur(img, (1, 23), 3)
    # Пороговое преобразование, но получаем множество мелких точек от одного сучка
    ret, img = cv2.threshold(img, 130, 255, cv2.THRESH_BINARY)
    # Размытие изображения, чтобы укрупнить точки
    img = cv2.GaussianBlur(img, (9, 9), 3)
    # Повторное пороговое преобразование
    ret, img = cv2.threshold(img, 250, 255, cv2.THRESH_BINARY)
    # Выделение контуров объектов0
    canny = cv2.Canny(img, 10, 10)
    # Инверсия изображения
    img = cv2.bitwise_not(img)
    count, her = cv2.findContours(canny, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    cv2.drawContours(img_color, count, -1, (255, 0, 0), 2)
    # Определение количество объектов
    labels, nbr_objects = measurements.label(img)
    print("     Количество объектов: ", nbr_objects)
    if nbr_objects <= 10:
        warehouse = 1
        print("     Фанера высшего сорта, склад №1.")
    elif nbr_objects <= 20:
        warehouse = 2
        print("     Фанера среднего сорта, склад №2.")
    elif nbr_objects > 20:
        warehouse = 3
        print("     Фанера низшего сорта, склад №3.")
    cv2.imshow("result", img_color)
    cv2.waitKey(0)

    # Сохранение результатов в файл
    desktop_path = os.path.join(os.path.join(os.environ['USERPROFILE']), 'Desktop')  # получаем путь к рабочему столу
    with open(os.path.join(desktop_path, 'results3.txt'), 'a') as file:
        if warehouse == 1:
            file.write('1')
        elif warehouse == 2:
            file.write('2')
        elif warehouse == 3:
            file.write('3')

# Открываем нужное изображение и передаем его в функцию main
if __name__ == '__main__':
    screenshot_path = "C:/Screenshots"
    image_path = os.path.join(screenshot_path, "*.png")  # поиск всех файлов с расширением .png в указанной папке

    images_lst = glob.glob(image_path)
    if len(images_lst) == 0:
        print("     Файлы с расширением .png в указанной папке не найдены.")
    else:
        for image_path in images_lst:
            print("     Имя файла: ", image_path)
            Image.open(image_path).show()
            main(image_path)