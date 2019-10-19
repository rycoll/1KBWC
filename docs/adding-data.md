---
title: Adding Data
layout: default
order_number: 4
---
Sometimes when you add new data to the code, you will need to make changes in multiple places.

{% assign sorted_guides = site.data-guides | sort: "title" %}
{% for guide in sorted_guides %}

<hr>

## {{ guide.title }}

{{ guide.content }}

{% endfor %}
