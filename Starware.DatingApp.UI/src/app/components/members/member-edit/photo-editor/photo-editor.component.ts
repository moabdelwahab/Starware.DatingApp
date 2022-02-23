import { ThisReceiver } from '@angular/compiler';
import { Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { map, take } from 'rxjs/operators';
import { MemberDto } from 'src/app/models/users/MemberDto';
import { PhotoDto } from 'src/app/models/users/photoDto';
import { UserDto } from 'src/app/models/users/UserDto';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit, OnChanges {
  @Output() photoChanged: EventEmitter<boolean> = new EventEmitter();
  @Input() member: MemberDto;
  @ViewChild('uploader') uploader: ElementRef;
  afuConfig: any;
  response: string;
  user: UserDto;
  apiUrl: string = environment.apiRoot;
  selectedFile: File;


  constructor(private userService: UsersService,
    private accountService: AccountService) {

  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log(this.member);
  }

  ngOnInit(): void {
    this.accountService.$currentUser.pipe(take(1)).subscribe(
      (user) => {
        this.user = user;
      }
    )
  }

  FileSelected(event: any) {
    this.selectedFile = <File>event.target.files[0];
  }

  onUpload() {
    var fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);
    this.userService.addPhoto(fd).subscribe((response) => {
      this.member.photos.push(response.data);
      this.uploader.nativeElement.value = null;
    });

  }


  onDelete(photo: PhotoDto) {
    this.userService.deletePhoto(photo.publicId).subscribe(() => {
      let photoToDeleteIndex = this.member.photos.indexOf(photo);
      if (photoToDeleteIndex > -1) {
        this.member.photos.splice(photoToDeleteIndex, 1);
      }
    })
  }

  makePhotoMain(photo: PhotoDto) {
    this.userService.setMainPhoto(photo.id).subscribe();

    var memberFromService = this.userService.usersResponse.data.find(x => x.userName == this.user.userName)
    memberFromService.photos.forEach(element => {
      element.isMain = false;
    });
    memberFromService.photoUrl = photo.url;
    memberFromService.photos.find(p => p.id == photo.id).isMain = true;
    this.user.photoUrl = photo.url;
    this.accountService.currentUser.next(this.user);

    let LocalStorageUser: UserDto = JSON.parse(localStorage.getItem('user'));
    LocalStorageUser.photoUrl = photo.url;
    localStorage.setItem('user',JSON.stringify(LocalStorageUser));
  }
}

