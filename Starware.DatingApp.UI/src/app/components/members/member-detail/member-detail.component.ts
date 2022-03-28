import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { UsersService } from 'src/app/services/users.service';
import { NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { NgxGalleryImage } from '@kolkov/ngx-gallery';
import { NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { UserDto } from 'src/app/models/users/UserDto';
import { MessagesService } from 'src/app/services/messages.service';
import { AccountService } from 'src/app/services/account.service';
import { take } from 'rxjs/operators';
import { Message } from 'src/app/models/users/Message';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { PresenceService } from 'src/app/services/presence.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, AfterViewInit ,OnDestroy {

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[] = [];
  user: MemberDto = new MemberDto();
  messages: Message[] = [];
  loggedUser: UserDto;
  @ViewChild('memberTabs',{static:true}) memberTabs: TabsetComponent;
  selectedTab: TabDirective;

  constructor(private userService: UsersService,
    private route: ActivatedRoute,
    private messageService: MessagesService,
    private accountService: AccountService,
    public presenceService: PresenceService) {
  }

  ngAfterViewInit(): void {
    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    });
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data.member.data;
    }, error => {
      console.log(error),
        () => {
          for (let i = 0; i < this.user.photos.length; i++) {
            this.addGelleryImages(this.user.photos[i].url)
          }
        }
    });

    this.accountService.$currentUser.pipe(take(1)).subscribe(response => {
      this.loggedUser = response;
    });

    this.setGalleryOptions();
  }

  setGalleryOptions() {
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

  messageAdded(e: Message) {
    this.messages.push(e);
  }

  onTabActivated(data: TabDirective) {
    this.selectedTab = data;
    if (this.selectedTab.heading == "Messages") { 
      this.messageService.createHubConnection(this.loggedUser,this.user.userName);
     }else
     {
       this.messageService.StopHubConnection();
     }
  }

  selectTab(index: number) {
    if (this.memberTabs) {
      this.memberTabs.tabs[index].active = true;
    }
  }

  addGelleryImages(photoUrl: string) {
    this.galleryImages.push({
      small: photoUrl,
      medium: photoUrl,
      big: photoUrl
    });
  }

  ngOnDestroy(): void {
    this.messageService.StopHubConnection();
  }

}
