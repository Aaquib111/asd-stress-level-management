
# A Stress Level Monitor for Students with Autism Spectrum Disorder!

This repo contains source code for an android application meant to be used with a Xiaomi Mi Band 3 to collect biometric data. It assumes knowledge of your band's secret key. Visit here to find your device's secret key: https://geekdoing.com/threads/how-to-get-the-auth-key-no-root.1446/

## So you want to know whats happening under the hood...
### TL:DR (Too long, didn't read)
The number of students requiring special education has been increasing for the past couple years. Of these, students with autism spectrum disorder (ASD) are prone to anxiety issues. Identifying stressors is one way to lower stress and stop potentially fatal consequences associated with prolonged anxiety and stressm, such as heart failure.

This project aims to use fitness bands, combined with an app and an AI that can detect periods of stress and alert caretakers, who can use patterns of stress to identify potential causes of it. The AI was found to be 98% accurate after tests with regular-education and special-education students.

### TS:WM (Too short, want more)
First of all, I love the enthusiasm! Here is a paper I wrote on this project, check it out! https://docs.google.com/document/d/1wW_hYSPY9jHtG4-tNGKYjuJAtgz5Ft9YQaNBvjQAYgU/edit?usp=sharing


# What's in store in the future
- Adding the XGBoost AI as a part of the app itself, using a C# wrapper (link: https://github.com/PicNet/XGBoost.Net)
- Creating a web-application using Django to enable caretakers to have a single website where they can access data of all their students
- Enabling the use of *any* fitness band with the application for ultimate accessibility
- iOS Support 

# Credits
[1] Can, Y. S., Chalabianloo, N., Ekiz, D., &amp; Ersoy, C. (2019). Continuous stress detection using wearable sensors in Real life: Algorithmic programming contest case study. Sensors, 19(8), 1849. https://doi.org/10.3390/s19081849 

- This project makes use of the WindesHeart SDK project to communicate with the MiBand 3. See more about them here: https://github.com/ictinnovaties-zorg/openwindesheart
