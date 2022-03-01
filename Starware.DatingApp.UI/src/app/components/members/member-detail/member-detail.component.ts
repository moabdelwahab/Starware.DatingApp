import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';
import { UserDto } from 'src/app/models/users/UserDto';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[] = [];
  user:MemberDto = new MemberDto();

  constructor(private userService:UsersService ,private route :ActivatedRoute) { }

  ngOnInit(): void {
    this.setGalleryOptions();
    this.getUserByUsername();
  }

  getUserByUsername()
  {
    this.userService.getUserByUsername(this.route.snapshot.paramMap.get('username')).subscribe((data)=>{
      this.user = data.data;
      if(this.user)
      {
        for (let i = 0; i < this.user.photos.length; i++) {
          this.addGelleryImages(this.user.photos[i].url)
        }
      }
    })
  }

  setGalleryOptions()
  {
    this.galleryOptions = [
      {
        width: '600px',
        height: '400px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '600px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false
      }
    ];
  }


  addGelleryImages(photoUrl:string)
  {
    this.galleryImages.push({
      small: photoUrl,
      medium: photoUrl,
      big: photoUrl
    });
  }
}
